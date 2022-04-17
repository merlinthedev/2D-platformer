using GXPEngine;
using GXPEngine.Core;


class BossBullet : AnimationSprite {

    private Vec2 velocity;
    private Vec2 position;

    public BossBullet(Vector2 position, Vec2 velocity) : base("sprites/ai/boss.png", 30, 1, addCollider: true) {
        this.position.x = position.x;
        this.position.y = position.y;
        this.velocity = velocity;
        collider.isTrigger = true;
        SetCycle(0, 30);
        SetOrigin(width / 2, height / 2);
    }

    void Update() { 
        position += velocity;
        updateScreenPosition();
        Animate(12 * Time.deltaTime / 1000f);
    }

    private void updateScreenPosition() {
        x = position.x;
        y = position.y;

        if (position.x < 0) this.Destroy();
        if (position.x > game.width) this.Destroy();
        if (position.y < 0) this.Destroy();
        if (position.y > game.height) this.Destroy();
    }

    public void OnCollision(GameObject obj) {
        if (obj is Player) {
            ((Player)obj).die(1);
            this.LateDestroy();
        }
    }
}

