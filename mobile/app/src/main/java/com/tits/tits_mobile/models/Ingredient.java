package com.tits.tits_mobile.models;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;

import java.util.ArrayList;

@JsonIgnoreProperties
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
    ArrayList<HazardStatement> hazardStatements;

    public Ingredient(){}

    public Ingredient(String polishName, String englishName) {
        this.polishName = polishName;
        this.englishName = englishName;
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
