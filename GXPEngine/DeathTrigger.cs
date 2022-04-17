using System;
using GXPEngine;
using TiledMapParser;

class DeathTrigger : AnimationSprite {


    

    public DeathTrigger(string fileName, int cols, int rows, TiledObject obj = null) : base("sprites/ui/empty.png", 1, 1, addCollider: true) {
        collider.isTrigger = true;
        this.visible = obj.GetBoolProperty("isVisible", true);
    }


    public void OnCollision(GameObject g) {
        if (g is Player) ((Player)g).die(0);
    }


    
}

