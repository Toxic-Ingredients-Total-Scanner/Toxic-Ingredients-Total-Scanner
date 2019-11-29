package com.tits.tits_mobile.models;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;

import java.io.Serializable;
import java.util.ArrayList;

@JsonIgnoreProperties
public class Product implements Serializable {
    private int id;
    private String gtin;
    private String productName;
    private String brand;
    private String brandOwner;
    private String manufacturer;
    private String coutnryOfOrigin;
    private String description;
    private String productImage;
    private String url;
    private String isLegal;
    private String modifiedDate;
    private ArrayList<Ingredient> ingredients;

    public String getProductName() {
        return productName;
    }

    public void setProductName(String productName) {
        this.productName = productName;
    }

    public String getBrand() {
        return brand;
    }

    public void setBrand(String brand) {
        this.brand = brand;
    }

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public ArrayList<Ingredient> getIngredients() {
        return ingredients;
    }

    public void setIngredients(ArrayList<Ingredient> ingredients) {
        this.ingredients = ingredients;
    }

    public Product(){
        super();
    }


    public String getProductImage() {
        return productImage;
    }

    @Override
    public String toString() {
        return "productName = " + productName + "\n\n" +
                "brand = " + brand + "\n\n" +
                "description = " + description + "\n\n";
    }
}


