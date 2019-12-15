package com.tits.tits_mobile;

public class IngredientEntry {
    private String mIngredientName;
    private String mIngredientHazardImage;

    public IngredientEntry(String mIngredientName, String mIngredientHazardImage) {
        this.mIngredientName = mIngredientName;
        this.mIngredientHazardImage = mIngredientHazardImage;
    }


    public String getmIngredientName() {
        return mIngredientName;
    }

    public void setmIngredientName(String mIngredientName) {
        this.mIngredientName = mIngredientName;
    }

    public String getmIngredientHazardImage() {
        return mIngredientHazardImage;
    }

    public void setmIngredientHazardImage(String mIngredientHazardImage) {
        this.mIngredientHazardImage = mIngredientHazardImage;
    }
}
