using GXPEngine;
using GXPEngine.Core;
using System;


class BetterSlimeBullet : Sprite {

    private Vec2 velocity;
    private Vec2 position;


    public BetterSlimeBullet(Vector2 position, Vec2 velocity) : base("sprites/ai/bullet.png", addCollider: true) {
        this.position.x = position.x;
        this.position.y = position.y;
        this.velocity = velocity;
        collider.isTrigger = true;
    }

    void Update() {
        position += velocity;
        updateScreenPosition();


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
        if (g is Player) ((Player)g).die(1);

    }
}