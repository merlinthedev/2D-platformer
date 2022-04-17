using GXPEngine;
using GXPEngine.Core;


class BossBullet : AnimationSprite {

    private Vec2 velocity;
    public Vec2 position;
    private Boss boss;

    public BossBullet(Vector2 position, Vec2 velocity, Boss boss) : base("sprites/ai/boss.png", 30, 1, addCollider: true) {
        this.position.x = position.x;
        this.position.y = position.y;
        this.velocity = velocity;
        this.boss = boss;
        collider.isTrigger = true;
        SetCycle(0, 30);
        SetOrigin(width / 2, height / 2);
        this.SetXY(0, 0);
    }

    void Update() { 
        SetScaleXY(0.15f);
        position += velocity;
        updateScreenPosition();
        Animate(12 * Time.deltaTime / 1000f);
    }

    private void updateScreenPosition() {
        x = position.x;
        y = position.y;

        if (position.x < 0) {
            boss.bulletcount--;
            this.Destroy();
        }
        //if (position.x > game.width) this.destroy();
        //if (position.y < 0) this.destroy();
        if (position.y > game.height) {
            boss.bulletcount--;
            this.Destroy();
        }
    }

    public void OnCollision(GameObject obj) {
        if (obj is Player) {
            ((Player)obj).die(1);
            this.LateDestroy();
        }
    }
}

