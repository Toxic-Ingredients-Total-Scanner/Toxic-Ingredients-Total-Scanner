package com.tits.tits_mobile;

import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.text.method.ScrollingMovementMethod;
import android.view.View;
import android.view.WindowManager;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.DeserializationFeature;
import com.fasterxml.jackson.databind.JsonMappingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.squareup.picasso.Picasso;
import com.tits.tits_mobile.HttpHandler.HttpGetRequest;
import com.tits.tits_mobile.models.Ingredient;
import com.tits.tits_mobile.models.Product;

import java.util.ArrayList;
import java.util.concurrent.ExecutionException;

import androidx.appcompat.app.AlertDialog;
import androidx.appcompat.app.AppCompatActivity;

public class EanActivity extends AppCompatActivity {

    TextView brand;
    TextView productName;
    TextView description;

    Button showIngredients;
    Button editItem;
    ImageView prodImg;
    String EAN = "";
    String pattern = "\\d{13}|\\d{8}";
    Product prod;
    ArrayList<Ingredient> ingList;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_ean);

        getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN, WindowManager.LayoutParams.FLAG_FULLSCREEN);

        brand = findViewById(R.id.brand);
        productName = findViewById(R.id.productName);
        description = findViewById(R.id.description);
        showIngredients = findViewById(R.id.showIngredients);
        prodImg = findViewById(R.id.img);
        editItem = findViewById(R.id.editItem);

        description.setMovementMethod(new ScrollingMovementMethod());

        EAN = getIntent().getStringExtra("EAN");

        if (EAN != null && EAN.matches(pattern)) {
            EAN = getIntent().getStringExtra("EAN");

            String myUrl = "http://217.182.79.249/api/Products/fullRequest?ean=" + EAN;
            final String result;


            HttpGetRequest getRequest = new HttpGetRequest(EanActivity.this);
            try {
                result = getRequest.execute(myUrl).get();

                if(result.equals("not found")){
                    AlertDialog.Builder builder = new AlertDialog.Builder(EanActivity.this);
                    builder.setMessage("Nie znaleziono produktu, chcesz dodać nowy?")
                            .setPositiveButton("Tak", new DialogInterface.OnClickListener() {
                                public void onClick(DialogInterface dialog, int id) {
                                    startActivity(new Intent(EanActivity.this, editProduct.class).putExtra("EAN", EAN));
                                }
                            })
                            .setNegativeButton("Nie", new DialogInterface.OnClickListener() {
                                public void onClick(DialogInterface dialog, int id) {
                                    startActivity(new Intent(EanActivity.this, MainActivity.class));
                                }
                            });
                    // Create the AlertDialog object and return it
                    AlertDialog alertDialog = builder.create();
                    alertDialog.show();


                } else {
                    prod = new ObjectMapper().configure(DeserializationFeature.FAIL_ON_UNKNOWN_PROPERTIES, false).readValue(result, Product.class);
                    if(prod.getIngredients() == null) {
                        AlertDialog.Builder builder1 = new AlertDialog.Builder(EanActivity.this);
                        builder1.setMessage("Znaleziono produkt bez składników, czy chcesz je dodać?")
                                .setPositiveButton("Tak", new DialogInterface.OnClickListener() {
                                    public void onClick(DialogInterface dialog, int id) {
                                        startActivity(new Intent(EanActivity.this, editProduct.class).putExtra("prod", prod));
                                    }
                                })
                                .setNegativeButton("Nie", new DialogInterface.OnClickListener() {
                                    public void onClick(DialogInterface dialog, int id) {
                                        Picasso.get().load(prod.getProductImage()).into(prodImg);
                                        brand.setText(prod.getBrand());
                                        productName.setText(prod.getProductName());
                                        description.setText(prod.getDescription());
                                    }
                                });

                        AlertDialog alertDialog1 = builder1.create();
                        alertDialog1.show();
                    } else {
                        Picasso.get().load(prod.getProductImage()).error(R.drawable.nophoto).into(prodImg);
                        brand.setText(prod.getBrand());
                        productName.setText(prod.getProductName());
                        description.setText(prod.getDescription());

                        ingList = prod.getIngredients();

                    }
                }

            } catch (ExecutionException e) {
                e.printStackTrace();
            } catch (InterruptedException e) {
                e.printStackTrace();
            } catch (JsonMappingException e) {
                e.printStackTrace();
            } catch (JsonProcessingException e) {
                e.printStackTrace();
            }
        } else {
            brand.setText("Nieprawidłowy kod EAN");
        }

        showIngredients.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Bundle extra = new Bundle();
                extra.putSerializable("arr", ingList);
                startActivity(new Intent(EanActivity.this, ingredientsList.class).putExtra("ingList", extra));
            }
        });

        editItem.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                startActivity(new Intent(EanActivity.this, editProduct.class).putExtra("prod", prod));
            }
        });

    }

}
