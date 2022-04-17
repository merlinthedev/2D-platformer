﻿using GXPEngine;
using System;


class EnergyBar : AnimationSprite {

    EasyDraw energyMeter;
    float[] energyParameter = { 0, 0 };
    public EnergyBar(float currentEnergy, float maxEnergy) : base("sprites/ui/empty.png", 1, 3, addCollider: false) {
        this.energy = currentEnergy;
        this.maxEnergy = maxEnergy;

        energyMeter = new EasyDraw("sprites/ui/white.png", false);
        AddChild(energyMeter);
        energyMeter.height = 3;
        energyMeter.SetXY(-8, 14);
        energyMeter.color = 0x00ff00;
    }


    public float energy {
        get => energyParameter[1];
        set {
            energyParameter[1] = value;
            updateThis();
        }
    }

    public float maxEnergy {
        get => energyParameter[0];
        set {
            energyParameter[0] = value;
            updateThis();
        }
    }


    private void updateThis() {
        byte R = (byte)(255 - (Mathf.Pow(energy / maxEnergy, 4f) * 0));
        byte G = (byte)(Mathf.Pow(energy / maxEnergy, 0.25f) * 180);
        byte B = 0;
        uint hex = (uint)((255 << 24) | ((byte)R << 16) | ((byte)G << 8) | ((Byte)B << 0));
        if (energyMeter != null) {
            energyMeter.color = hex;
            energyMeter.width = (int)(energy / maxEnergy * 20);
        }
        currentFrame = 2 - (int)(energy / (maxEnergy / 3));
    }
}
