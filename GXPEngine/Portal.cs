using System;
using GXPEngine;


class Portal : AnimationSprite {

    private Level level;

    public Portal(int x, int y, Level level) : base("maps/portalgreen.png", 8, 3, addCollider: false) {

        this.x = x;
        this.y = y;
        this.level = level;
        SetCycle(0, 8);
        SetScaleXY(4f);

    }


    void Update() {
        if (((MyGame)game).getLevelName() == "maps/resizedone.tmx") {
            if (level.player.y <= 1575 && level.player.y >= 1400) {
                if (level.player.x <= 170) {
                    this.SetXY(-1000, -1000);
                    ((MyGame)game).destroyAll();
                    ((MyGame)game).loadLevel("maps/boss.tmx");
                }
            }
        }



        Animate(12 * Time.deltaTime / 1000f);
    }

}
