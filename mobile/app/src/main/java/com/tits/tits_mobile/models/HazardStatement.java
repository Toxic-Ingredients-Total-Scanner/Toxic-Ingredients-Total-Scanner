package com.tits.tits_mobile.models;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;

@JsonIgnoreProperties
public class HazardStatement {
    int Id;
    String Code;
    String descriptionPolish;
    String descriptionEnglish;
}


