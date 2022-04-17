using System;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;

class BetterSlime : AnimationSprite {


    private TiledObject obj;
    private float left, right;
    private float ySpeed, xSpeed;
    private float movementSpeed;
    private float gravity = 20f;
    private float jumpSpeed = 2.4f;
    private bool flipped = false;
    private bool grounded = false;


    public BetterSlime(string fileName, int cols, int rows, TiledObject obj) : base("sprites/ai/boss.png", 30, 1, addCollider: true) {
        this.obj = obj;
        left = obj.GetFloatProperty("p1", 1);
        right = obj.GetFloatProperty("p2", 1);
        movementSpeed = obj.GetFloatProperty("ms", 1);
        this.collider.isTrigger = true;

    }

    void Update() {
        SetScaleXY(0.25f);
        Animate(12 * Time.deltaTime / 1000f);
        slimeMovement();
        shoot();
    }

    public void OnCollision(GameObject g) {
        if (g is Player) ((Player)g).die(1);

    }

    private void shoot() {
        int bulletcount = game.FindObjectsOfType<BetterSlimeBullet>().Length;
        Console.WriteLine(bulletcount);
        Vector2 a = this.TransformPoint(0, 0);
        Vec2 targetVelocity = new Vec2(0, 0);
        targetVelocity.x = flipped ? 1 : -1;
        if (bulletcount < 1) {
            BetterSlimeBullet b = new BetterSlimeBullet(a, targetVelocity * 5);

            game.LateAddChild(b);
        }
    }

    private void slimeMovement() {
        if (x < left && !flipped) flipped = true;
        if (x > right && flipped) flipped = false;


        //Console.WriteLine("x: " + this.x + " y: " + this.y + " left: " + left + " right: " + right);

        xSpeed = (flipped ? 1 : -1) * movementSpeed;
        ySpeed += gravity * Time.deltaTime / 1000f;
        if (grounded) {
            ySpeed = -jumpSpeed * 3;
            grounded = false;
        }
        Move(xSpeed, 0);

        Collision collision;
        collision = MoveUntilCollision(0, ySpeed);
        if (collision != null) {
            if (ySpeed > 0) {
                grounded = true;
            }
            ySpeed = 0;
            y = (int)y;
        }


    }
}

