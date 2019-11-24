package com.tits.tits_mobile.models;

import java.util.ArrayList;

public class Ingredient {
    int id = 0;
    int pubChemCID = 0;
    String polishName;
    String englishName;
    String molecularFormula = null;
    String strucutreImageUrl = null;
    String ghsClasificationRaportUrl = null;
    String pubChemUrl = null;
    String wikiUrl = null;
    ArrayList<HazardStatement> list;

    public Ingredient(String polishName, String englishName) {
        this.id = 0;
        this.pubChemCID = 0;
        this.polishName = polishName;
        this.englishName = englishName;
        this.molecularFormula = null;
        this.strucutreImageUrl = null;
        this.ghsClasificationRaportUrl = null;
        this.pubChemUrl = null;
        this.wikiUrl = null;
        this.list = null;
    }

    public String getPolishName() {
        return polishName;
    }

    public void setPolishName(String polishName) {
        this.polishName = polishName;
    }

    public String getEnglishName() {
        return englishName;
    }

    public void setEnglishName(String englishName) {
        this.englishName = englishName;
    }
}
