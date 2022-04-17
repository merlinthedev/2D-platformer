using GXPEngine;
using System;
using System.Drawing;
using TiledMapParser;


class Level : Pivot {
    private TiledLoader loader;
    private TiledObject obj;
    private EasyDraw draw;


    public Player player;
    public NPC npc;
    public UI ui;



    public Level(UI ui) : base() {
        this.ui = ui;
        draw = new EasyDraw(50, 50, addCollider: false);
        spawnPortals();
    }

    public void LoadMapFile(string filename) {
        loader = new TiledLoader(filename, this);
        loader.OnObjectCreated += createObjectCallback;
    }



    public void LoadLevel() {
        loader.autoInstance = false;
        loader.addColliders = false;

        if (loader.NumImageLayers != 0) {
            loader.LoadImageLayers();
        }

        loader.rootObject = this;
        loader.AddManualType(new string[] { "Player" });
        loader.AddManualType(new string[] { "NPC" });
        loader.autoInstance = true;

        loader.addColliders = true;
        loader.LoadObjectGroups();
        loader.LoadTileLayers();




        Console.WriteLine("Level loaded");
    }

    public Player getPlayer() {
        return this.player;
    }

    public void createObjectCallback(Sprite sprite, TiledObject obj) {
        if (obj.Type == "Player" && player == null) { //check so we dont get a player for every NPC object in the tiled map
            this.obj = obj;
            player = new Player(obj);
            LateAddChild(player);
            player.SetXY(obj.X, obj.Y);
            player.SetScaleXY(0.25f);
            player.initializePlayer(this);
        }
        if (obj.Type == "NPC" && npc == null) {
            this.obj = obj;
            npc = new NPC(obj);
            LateAddChild(npc);
            npc.SetXY(obj.X, obj.Y);
            npc.initializeObject(this);
            npc.SetScaleXY(0.25f);
        }

    }


    private void b() {
        //draw.Clear(Color.Red);
        //draw.SetXY(player.x, player.y);
        //this.AddChild(draw);

        int xb = 512;
        int yb = 256;

        if (((MyGame)game).getLevelName() == "maps/boss.tmx") yb = 128;

        if (player.x + x < xb) {
            x = xb - player.x;

        }

        if (player.x + x > game.width - xb) {
            x = game.width - xb - player.x;
        }

        if (player.y + y < yb) {
            y = yb - player.y;
        }

        if (player.y + y > game.height - yb) {
            y = game.height - yb - player.y;
        }



    }

    private void spawnPortals() {
        if (((MyGame)game).getLevelName() == "maps/resizedone.tmx") {
            Portal portal = new Portal(0, 1400, this);
            LateAddChild(portal);

        }

        if (((MyGame)game).getLevelName() == "maps/two.tmx") {
            Portal portal = new Portal(0, 420, this);
            LateAddChild(portal);

        }
    }

    private void Update() {

        b();

        


        if (Input.GetKeyDown(Key.RIGHT)) x -= 100;
        if (Input.GetKeyDown(Key.LEFT)) {
            Console.WriteLine("level player position: x: " + player.x + " y: " + player.y + " engine player position: " + player.TransformPoint(0, 0));
            Console.WriteLine("Level.width: " + this.x + " , " + this.y);
        }
        //Console.WriteLine(this.FindObjectsOfType<Player>().Length);

    }
}

