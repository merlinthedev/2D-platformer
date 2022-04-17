using GXPEngine;
using GXPEngine.Core;
using System;
using System.Drawing;
using TiledMapParser;


class Player : EasyDraw {


    const int playerWidth = 140;
    const int playerHeight = 260;



    private AnimationSprite animationSprite;
    private TiledObject obj;
    public Level level;
    private EnergyBar energyBar;
    private EasyDraw draw;
    private EasyDraw text;




    private float movementSpeed = 320;
    private float walkSpeed = 320f;
    private float dashSpeed = 18000f;
    private float jumpSpeed = 2.4f;
    private float velocityMargin = 0.1f;
    private float xSpeed, ySpeed = 0;
    private float gravity = 20f;
    private float currentEnergy = 100f;
    private float maxEnergy = 100f;
    private float energyCost = 50f;

    private bool grounded = false;
    private bool isCollided = false;
    private bool isDead = false;




    public Player(TiledObject obj) : base(playerWidth, playerHeight, true) {
        this.obj = obj;
        collider.isTrigger = true;
    }


    private void initializeAnimation() {
        animationSprite = new AnimationSprite("sprites/player/spritesheet.png", 20, 4, addCollider: false);
        LateAddChild(animationSprite);
        animationSprite.SetOrigin(animationSprite.width / 2, animationSprite.height);
        animationSprite.SetXY(45, 370); //set animation sprite offset for a better player hitbox
        animationSprite.currentFrame = 0;
        animationSprite.SetCycle(0, 20);

    }

    public void initializePlayer(Level level) {

        this.level = level;
        initializeAnimation();
        this.SetOrigin(width / 2, height / 2);

        //uncomment to draw the player hitbox
        Clear(255, 0, 0, 255);


        energyBar = new EnergyBar(currentEnergy, maxEnergy);
        energyBar.SetOrigin(width / 2, 0);
        energyBar.SetScaleXY(10f);
        energyBar.SetXY(game.width / 2, -100);
        level.ui.LateAddChild(energyBar);


    }

    public void die(int index) {
        isDead = true;
        level.npc.playerDeath(index);

    }


    private void playerAnimation() {
        bool flipped = false;


        if (xSpeed < 0) {
            flipped = true;
        }

        if (xSpeed != 0) {
            animationSprite.Mirror(flipped, false);
        }

        if (ySpeed > velocityMargin) {
            animationSprite.SetCycle(40, 8);
        } else if (xSpeed > velocityMargin || xSpeed < -velocityMargin) {
            animationSprite.SetCycle(20, 20);

        } else {
            animationSprite.SetCycle(0, 20);
        }


        if (!isDead) {
            playerKeyboardInput(flipped);
        }
    }

    private void playerKeyboardInput(bool flipped) {
        bool hasEnergy = currentEnergy > energyCost;




        movementSpeed = Input.GetKeyDown(Key.LEFT_SHIFT) && hasEnergy ? dashSpeed : walkSpeed;
        if (movementSpeed == dashSpeed) {
            currentEnergy -= maxEnergy - 4f;
        } else if (currentEnergy < maxEnergy) {
            currentEnergy += 0.5f;
        }

        xSpeed = ((Input.GetKey(Key.D) ? 1 : 0) - (Input.GetKey(Key.A) ? 1 : 0)) * movementSpeed * (Time.deltaTime / 1000f);
        ySpeed += gravity * Time.deltaTime / 1000f;

        if ((Input.GetKeyDown(Key.W) || (Input.GetKeyDown(Key.SPACE))) && grounded) {
            ySpeed = -jumpSpeed * 4;

        }


        Collision collision;
        collision = MoveUntilCollision(0, ySpeed);
        if (collision != null) {
            isCollided = true;
            if (ySpeed > 0) {
                grounded = true;
            }

            ySpeed = 0;
            y = (int)y;
        } else {
            grounded = false;
            isCollided = false;
        }

        collision = MoveUntilCollision(xSpeed, 0);
        if (collision != null) {
            isCollided = true;
            xSpeed = 0;
            x = (int)x;
            if (flipped) {
                x = (int)x + 40;
            }
        } else {
            isCollided = false;
        }



        shoot(flipped);


        //Console.WriteLine("Flipped? " + flipped);
    }

    private void updateUI() {
        currentEnergy = Mathf.Max(currentEnergy, 0);
        energyBar.energy = currentEnergy;


    }

    private void shoot(bool flipped) {

        if (Input.GetMouseButtonDown(0)) {
            Vector2 a = this.TransformPoint(0, 0);
            Vec2 tv = new Vec2(Input.mouseX - a.x, Input.mouseY - a.y);
            Console.WriteLine("Mouse position: " + "X: " + Input.mouseX + " Y: " + Input.mouseY + " player position X: " + x + " Y: " + y);
            Console.WriteLine("Distance between mouse and player vector: " + tv);
            float r = tv.GetAngleDegrees() - rotation;
            Bullet bullet = new Bullet(a, Vec2.GetUnitVectorDeg(r) * 15);
            bullet.SetScaleXY(0.2f);
            game.LateAddChild(bullet);

            //draw = new EasyDraw(50, 50, false);
            //draw.Clear(Color.Red);
            //draw.SetXY(x, y);
            //this.AddChild(draw);
        }
    }

    private void Update() {

        if (Input.GetKeyDown(Key.F)) { this.x = obj.X; this.y = obj.Y; isDead = false; }
        playerAnimation();
        updateUI();


        //Console.WriteLine(isDead);
        GameObject[] collisions = GetCollisions();
        for (int i = 0; i < collisions.Length; i++) {
            if (collisions[i] is Portal) {
                Console.WriteLine("Hit test");
            }
        }




        animationSprite.Animate(16f * Time.deltaTime / 1000f);

        //Console.WriteLine("Player position: " + "X: " + x + " Y: " + y + " Grounded: " + grounded + " isCollided: " + isCollided);
        //Console.WriteLine("Mouse position: " + "X: " + Input.mouseX + " Y: " + Input.mouseY);

    }
}

