using System;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;

class Slime : AnimationSprite {

    private TiledObject obj;
    private float left, right;
    private float ySpeed, xSpeed;
    private float gravity = 20f;
    private float jumpSpeed = 2.4f;
    private bool flipped = false;
    private bool grounded = false;
    

    public Slime(string fileName, int cols, int rows, TiledObject obj) : base("sprites/ai/slime.png", 30, 1, addCollider: true) { 
        this.obj = obj;
        left = obj.GetFloatProperty("p1", 1);
        right = obj.GetFloatProperty("p2", 1);
        this.collider.isTrigger = true;
    }

    void Update() { 
        SetScaleXY(0.25f);
        Animate(12 * Time.deltaTime / 1000f);
        slimeMovement();
        
    }

    public void OnCollision(GameObject g) {
        if (g is Player) ((Player)g).die(1);
        
    }

    private void slimeMovement() {
        if(x < left && !flipped) flipped = true;
        if(x > right && flipped) flipped = false;

        
        //Console.WriteLine("x: " + this.x + " y: " + this.y + " left: " + left + " right: " + right);

        xSpeed = flipped ? 1 : -1;
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

