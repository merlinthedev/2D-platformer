using GXPEngine;

class Menu : Pivot {
    private Button playButton;
    private Button quitButton;

    public Menu() : base() {
        playButton = new Button("sprites/Menu Buttons/Large Buttons/Large Buttons/Start Button.png");
        LateAddChild(playButton);
        playButton.x = (game.width / 2);
        playButton.y = (game.height / 3);
        playButton.SetScaleXY(2f);

        quitButton = new Button("sprites/Menu Buttons/Large Buttons/Large Buttons/Quit Button.png");
        LateAddChild(quitButton);
        quitButton.x = (game.width / 2);
        quitButton.y = (game.height / 3) * 2;
        quitButton.SetScaleXY(2f);
    }

    private void Update() {
        if (playButton.HitTestPoint(Input.mouseX, Input.mouseY) && Input.GetMouseButtonDown(0)) { ((MyGame)game).loadLevel("maps/levelone.tmx"); this.Destroy(); }
        if (quitButton.HitTestPoint(Input.mouseX, Input.mouseY) && Input.GetMouseButtonDown(0)) {
            ((MyGame)game).Destroy();
        }
    }
}
