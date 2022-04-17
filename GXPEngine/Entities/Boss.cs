using System;
using System.Drawing;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;

class Boss : AnimationSprite {

    private TiledObject obj;
    private EasyDraw draw;
    private float left, right;
    private float ySpeed, xSpeed;
    private float gravity = 30f;
    private float jumpSpeed = 10f;
    private bool flipped = false;
    private bool grounded = false;

    public int health = 100000;


    public Boss(string fileName, int cols, int rows, TiledObject obj) : base("sprites/ai/boss.png", 30, 1, addCollider: true) {
        this.obj = obj;
        left = obj.GetFloatProperty("p1", 1);
        right = obj.GetFloatProperty("p2", 1);
        this.collider.isTrigger = true;
        draw = new EasyDraw(200, 200, false);
        AddChild(draw);
    }

    void Update() {
        SetCycle(0, 30);
        Animate(12 * Time.deltaTime / 1000f);
        movement();

        draw.SetXY(-100, -300);
        draw.Clear(Color.Red);
        draw.Fill(255);
        draw.TextSize(64);
        draw.TextAlign(CenterMode.Center, CenterMode.Center);
        draw.Text(health.ToString(), 100, 50);
    }

    public void takeDamage(int damage) {
        health -= damage;

        if (health < 0) {
            this.LateDestroy();
        }
    }

    public void OnCollision(GameObject obj) {
        if (obj is Player) ((Player)obj).die(1);
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

