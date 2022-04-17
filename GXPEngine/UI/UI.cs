using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class UI : EasyDraw {


    public UI(int width, int height) : base(width, height, false) {
        TextAlign(CenterMode.Center, CenterMode.Center);
        ShapeAlign(CenterMode.Center, CenterMode.Center);
        TextSize(10);
    }

}

