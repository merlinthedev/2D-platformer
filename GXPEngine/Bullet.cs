using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using GXPEngine.Core;


class Bullet : Sprite {


    public Vec2 _position {
        get {
            return position;
        }
    }

    public Vec2 velocity;

    private Vec2 position;
    public Bullet(Vector2 position, Vec2 velocity) : base("circle.png", addCollider: true) {
        this.position.x = position.x;
        this.position.y = position.y;
        this.velocity = velocity;
        collider.isTrigger = true;

    }

    private void updateScreenPosition() {
        x = position.x;
        y = position.y;

        if (position.x < 0) this.Destroy();
        if (position.x > game.width) this.Destroy();
        if (position.y < 0) this.Destroy();
        if (position.y > game.height) this.Destroy();
    }


    public void OnCollision(GameObject g) {
        if (g is Slime) {
            ((Slime)g).LateDestroy();
            this.LateDestroy();
        }
        if (g is BetterSlime) {
            ((BetterSlime)g).LateDestroy();
            this.LateDestroy();
        } 
        if (g is Boss) ((Boss)g).takeDamage(1);
    }




    void Update() {
        position += velocity;
        updateScreenPosition();
    }
}

