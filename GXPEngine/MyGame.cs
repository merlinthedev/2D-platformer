using GXPEngine;
using System;


class MyGame : Game {

    public Level level;
    private UI ui;
    private Menu menu;

    public string levelname = "maps/resizedone.tmx";

    public MyGame() : base(1280, 720, false, false)     //7680 4320
    {

        this.ShowMouse(true);
        this.targetFps = 60;
        //createMenu();
        loadLevel(levelname);
        debug();
        Console.WriteLine("Game initialized");
    }

    public void destroyAll() {
        level.LateDestroy();
        ui.LateDestroy();
    }

    public void loadLevel(string filename) {
        createUI();
        level = new Level(ui);
        LateAddChild(level);
        level.LoadMapFile(filename);
        level.LoadLevel();


    }

    private void createMenu() {
        menu = new Menu();
        AddChild(menu);
    }

    public void createUI() {
        ui = new UI(width, height / 8);
        LateAddChild(ui);
    }

    public string getLevelName() {
        return levelname;
    }

   

    void Update() {
        //Console.WriteLine(this.levelname);
    }

    private void debug() {
        Console.WriteLine("UI count: " + FindObjectsOfType<UI>().Length);
        Console.WriteLine("Level count: " + FindObjectsOfType<Level>().Length);
        Console.WriteLine("Player count: " + FindObjectsOfType<Player>().Length);
        Console.WriteLine("TutorialSlime count: " + FindObjectsOfType<TutorialSlime>().Length);
        Console.WriteLine("Width: " + width + " Height: " + height + " Height / 4: " + height / 4);
    }

    static void Main() {
        new MyGame().Start();
    }
}