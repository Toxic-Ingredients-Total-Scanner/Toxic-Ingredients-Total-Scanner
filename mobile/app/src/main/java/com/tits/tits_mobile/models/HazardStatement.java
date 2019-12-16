package com.tits.tits_mobile.models;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;

import java.io.Serializable;

@JsonIgnoreProperties
public class HazardStatement implements Serializable {
    int Id;
    String Code;
    String descriptionPolish;
    String descriptionEnglish;

    public HazardStatement(){
        super();
    }

    public int getId() {
        return Id;
    }

    public void setId(int id) {
        Id = id;
    }

    public String getCode() {
        return Code;
    }

    public void setCode(String code) {
        Code = code;
    }

    public String getDescriptionPolish() {
        return descriptionPolish;
    }

    public void setDescriptionPolish(String descriptionPolish) {
        this.descriptionPolish = descriptionPolish;
    }

    public String getDescriptionEnglish() {
        return descriptionEnglish;
    }

    public void setDescriptionEnglish(String descriptionEnglish) {
        this.descriptionEnglish = descriptionEnglish;
    }
}


