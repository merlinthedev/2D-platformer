using System;
using System.Drawing;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;

class Boss : AnimationSprite {


    


    private Level level;
    private Player player;
    private TiledObject obj;
    private EasyDraw draw;
    private float left, right;
    private float ySpeed, xSpeed;
    private float gravity = 30f;
    private float jumpSpeed = 10f;
    private bool flipped = false;
    private bool grounded = false;
    private bool isDead = false;
    public int bulletcount = 1;

    public int health = 3000;

    public Boss(string fileName, int cols, int rows, TiledObject obj) : base("sprites/ai/boss.png", 30, 1, addCollider: true) {
        this.obj = obj;
        this.level = ((MyGame)game).level;
        player = level.getPlayer();
        left = obj.GetFloatProperty("p1", 1);
        right = obj.GetFloatProperty("p2", 1);
        this.collider.isTrigger = true;
        draw = new EasyDraw(100, 70, false);

        

    }

   

    void Update() {
        SetCycle(0, 30);
        Animate(12 * Time.deltaTime / 1000f);

        draw.Clear(Color.White);
        draw.SetOrigin(width / 2, height / 2);
        draw.SetXY(width / 2, 300);
        draw.Fill(0);
        draw.TextAlign(CenterMode.Center, CenterMode.Center);
        draw.Text(health.ToString());
        AddChild(draw);



        if (!isDead) {
            movement();
            shoot();

        }
        //Console.WriteLine("BossBullet Count: " + game.FindObjectsOfType<BossBullet>().Length);

    }

    public void takeDamage(int damage) {
        health -= damage;

        if (health < 0) {
            isDead = true;
            die();
        }
    }

    public void OnCollision(GameObject obj) {
        if (obj is Player) {
            if (!isDead) {
                ((Player)obj).die(1);
            }
        }
    }

    private void die() {
        x = 500; 
        y = 800;  
        level.npc.won();
    }

    private void shoot() {
        //Vector2 a = new Vector2(this.x, this.y);
        //Vec2 tv = new Vec2(level.player.x - a.x, level.player.y - a.y);
        //float r = tv.GetAngleDegrees();
        //for (int i = 0; i < bulletcount; i++) {
        //    BossBullet b = new BossBullet(new Vector2(obj.X, obj.Y), Vec2.GetUnitVectorDeg(r) * 1);
        //    b.SetScaleXY(2f);
        //    game.LateAddChild(b);
        //}

        //Console.WriteLine(obj.X + " " + obj.Y);

        Console.WriteLine("Bulletcount: " + bulletcount + " actual bullet count: " + game.FindObjectsOfType<BossBullet>().Length);
        bulletcount = game.FindObjectsOfType<BossBullet>().Length;

        if (bulletcount < 2) {
            Vector2 pos = this.TransformPoint(0, 0);
            Vector2 pp = level.player.TransformPoint(0, 0);
            pos.y -= 100;
            Vec2 tv = new Vec2(pp.x - pos.x, pp.y - pos.y);
            Console.WriteLine(tv);
            float r = tv.GetAngleDegrees();

            BossBullet b = new BossBullet(pos, Vec2.GetUnitVectorDeg(r) * 10, this);
            b.SetScaleXY(1f);
            game.LateAddChild(b);
            bulletcount++;


        }

    }

    private void movement() {
        if (x < left && !flipped) flipped = true;
        if (x > right && flipped) flipped = false;

        xSpeed = flipped ? 1 : -1;
        ySpeed += gravity * Time.deltaTime / 1000f;
        if (grounded) {
            ySpeed = -jumpSpeed * 2;
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

