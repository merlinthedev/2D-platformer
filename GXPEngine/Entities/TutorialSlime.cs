using System;
using GXPEngine;
using TiledMapParser;



class TutorialSlime : AnimationSprite {

    private TiledObject obj;

    public TutorialSlime(string fileName, int cols, int rows, TiledObject obj) : base("sprites/ai/slime.png", 30, 1, addCollider:true) {
        this.obj = obj;
        SetCycle(0, 30);
        Console.WriteLine("Slime instance");
        
    }

    private void collisionCheck() {
        GameObject[] collisions = GetCollisions();
        for (int i = 0; i < collisions.Length; i++) {
            if (collisions[i] is Bullet) {
                collisions[i].Destroy();
                this.Destroy();

            }
        }
    }

    private void Update() {
        
        collisionCheck();
        SetScaleXY(0.25f);
        Animate(12 * Time.deltaTime / 1000f);
    }

}

