package com.tits.tits_mobile.models;

import com.fasterxml.jackson.annotation.JsonFormat;
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
    private String countryOfOrigin;
    private String description;
    private String productImage;
    private String base64Image;
    private String url;
    private Boolean isLegal;
    //private String modifiedDate;
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

    public Product(){
        super();
    }

    public String getGtin() {
        return gtin;
    }

    public void setGtin(String gtin) {
        this.gtin = gtin;
    }

    public void setIngredients(ArrayList<Ingredient> ingredients) {
        this.ingredients = ingredients;
    }


    public String getProductImage() {
        return productImage;
    }

    public String getBrandOwner() {
        return brandOwner;
    }

    public void setBrandOwner(String brandOwner) {
        this.brandOwner = brandOwner;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getManufacturer() {
        return manufacturer;
    }

    public void setManufacturer(String manufacturer) {
        this.manufacturer = manufacturer;
    }

    public String getCountryOfOrigin() {
        return countryOfOrigin;
    }

    public void setCountryOfOrigin(String countryOfOrigin) {
        this.countryOfOrigin = countryOfOrigin;
    }

    public void setProductImage(String productImage) {
        this.productImage = productImage;
    }

    public String getBase64Image() {
        return base64Image;
    }

    public void setBase64Image(String base64Image) {
        this.base64Image = base64Image;
    }

    public String getUrl() {
        return url;
    }

    public void setUrl(String url) {
        this.url = url;
    }

    public Boolean getLegal() {
        return isLegal;
    }

    public void setLegal(Boolean legal) {
        isLegal = legal;
    }

    @Override
    public String toString() {
        return "productName = " + productName + "\n\n" +
                "brand = " + brand + "\n\n" +
                "description = " + description + "\n\n";
    }
}


