using GXPEngine;
using TiledMapParser;
using System.Drawing;

class NPC : EasyDraw {
    const int WIDTH = 140;
    const int HEIGHT = 260;


    private AnimationSprite sprite;
    private Level level;
    private TiledObject obj;
    private EasyDraw text;
    private string[] dialog;
    public int i = 2;

    public NPC(TiledObject obj) : base(WIDTH, HEIGHT, addCollider: false) {
        this.obj = obj;

        dialog = new string[] {
            "You are not supposed to be here. \nBack to the beginning you go.\n Press F to respawn.",
            "You died!\n Back to the beginning you go.\n Press F to respawn",
            "Welcome! \nPress E to skip through dialog",
            "Use A and D to move left and right",
            "Use W to jump",
            "Be careful and don't fall off the world!",
            "These are slimes",
            "You can shoot at them by aiming\n and clicking LMB",
            "But be careful!\n if they hit you, you might die!",
            "You have one more ability.\n Above you there is\n an energy bar.",
            "When this bar contains enough energy for a dash,\n you can dash with left shift.",
            "Using this ability costs energy,\n so use it wisely.",
            "But do not worry,\n energy regenerates passively.",
            "This is a portal. \nJump into the portal to advance to the next level.\n Best of luck!",
            " ",
            "Congrats",
            "You have completed the game :D",
            "You have proven your might and conquered\n the challenges of this world.",
            "If you wish to play again, press Q",
            " "
        };






        text = new EasyDraw(1600, 300, false);
        LateAddChild(text);

    }

    private void initializeAnimation() {
        sprite = new AnimationSprite("sprites/player/npc.png", 20, 4, addCollider: false);
        LateAddChild(sprite);
        sprite.Mirror(true, false);
        sprite.SetOrigin(sprite.width / 2, sprite.height);
        sprite.SetXY(65, 370);
        sprite.currentFrame = 0;
        sprite.SetCycle(0, 20);
    }

    public void initializeObject(Level level) {
        this.level = level;
        initializeAnimation();

        //Clear(255, 0, 0, 255);

    }

    void Update() {
        //if (Input.GetKeyDown(Key.LEFT)) {
        //    System.Console.WriteLine("level.player.x: " + level.player.x + " y: " + level.player.y);
        //    System.Console.WriteLine("this.x: " + this.x + " this.y: " + this.y);
        //    playerDeath(0);
        //}
        drawText();
        sprite.Animate(8 * Time.deltaTime / 1000f);

    }

    public void playerDeath(int index) {
        this.SetXY(level.player.x + 200, level.player.y);
        if (index == 0) {
            i = 0;
        }
        if (index == 1) {
            i = 1;
        }
    }

    public void won() {
        i = 15;
    }



    private void drawText() {
        if (Input.GetKeyDown(Key.F)) { i = 2; this.SetXY(obj.X, obj.Y); }
        if (Input.GetKeyDown(Key.E)) i++;
        if (Input.GetKeyDown(Key.Q) && i > 17) {
            ((MyGame)game).destroyAll();
            ((MyGame)game).loadLevel("maps/resizedone.tmx");
            ((MyGame)game).levelname = "maps/resizedone.tmx";
        }
        if (i == 0 || i == 1) {
            text.SetXY(0, -400);
            text.Clear(Color.White);
            text.Fill(0);
            text.TextSize(48);
            text.TextAlign(CenterMode.Min, CenterMode.Min);
            text.Text(dialog[i], 0, 50);
        }

        if (i > 1 && i < 7) {
            text.SetXY(0, -400);
            text.Clear(Color.White);
            text.Fill(0);
            text.TextSize(48);
            text.TextAlign(CenterMode.Min, CenterMode.Min);
            text.Text(dialog[i], 0, 50);
        }
        if (i > 5 && i < 9) {

            this.SetXY(1900, 870);
            text.Clear(Color.White);
            text.Text(dialog[i], 0, 50);
        }
        if (i > 8 && i < 13) {
            sprite.Mirror(false, false);
            this.SetXY(1300, 1600);
            text.Clear(Color.White);
            text.Text(dialog[i], 0, 50);

        }

        if (i == 13) {
            this.SetXY(100, 1350);
            text.Clear(Color.White);
            text.Text(dialog[i], 0, 50);
        }

        if (i > 13) {
            this.SetXY(-1000, -1000);
        }

        if (i > 14) {
            this.SetXY(500, 500);
            level.player.SetXY(x - 200, y);
            text.Clear(Color.White);
            text.Text(dialog[i], 0, 50);
            level.player.ySpeed = 0;
        }


    }

}
