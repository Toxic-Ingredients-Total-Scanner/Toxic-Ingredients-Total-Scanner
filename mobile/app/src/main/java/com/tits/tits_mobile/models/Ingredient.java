package com.tits.tits_mobile.models;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;

import java.io.Serializable;
import java.util.ArrayList;

@JsonIgnoreProperties
public class Ingredient implements Serializable {
    int id = 0;
    int pubChemCID = 0;
    String polishName;
    String englishName;
    String molecularFormula = null;
    String strucutreImageUrl = null;
    String ghsClasificationRaportUrl = null;
    String pubChemUrl = null;
    String wikiUrl = null;

    public ArrayList<HazardStatement> getHazardStatements() {
        return hazardStatements;
    }

    ArrayList<HazardStatement> hazardStatements;

    public Ingredient(){
        super();
    }

    public Ingredient(String polishName) {
        this.polishName = polishName;
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

    public String getMolecularFormula() {
        return molecularFormula;
    }

    public void setMolecularFormula(String molecularFormula) {
        this.molecularFormula = molecularFormula;
    }

    public String getStrucutreImageUrl() {
        return strucutreImageUrl;
    }

    public void setStrucutreImageUrl(String strucutreImageUrl) {
        this.strucutreImageUrl = strucutreImageUrl;
    }

    public String getPubChemUrl() {
        return pubChemUrl;
    }

    public void setPubChemUrl(String pubChemUrl) {
        this.pubChemUrl = pubChemUrl;
    }

    public String getWikiUrl() {
        return wikiUrl;
    }

    public void setWikiUrl(String wikiUrl) {
        this.wikiUrl = wikiUrl;
    }

    public void setHazardStatements(ArrayList<HazardStatement> hazardStatements) {
        this.hazardStatements = hazardStatements;
    }
}
