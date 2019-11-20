package com.tits.tits_mobile;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

import java.util.concurrent.ExecutionException;

import androidx.appcompat.app.AppCompatActivity;

public class EanActivity extends AppCompatActivity {

    TextView eanTxtView;
    Button anotherScan;
    String EAN = "";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_ean);

        eanTxtView = findViewById(R.id.eanTxtView);
        anotherScan = findViewById(R.id.anotherScan);

        if (getIntent().getStringExtra("EAN") != null) {
            EAN = getIntent().getStringExtra("EAN");

            String myUrl = "http://217.182.79.249/api/products/getByEan?ean=" + EAN;   //String to place our result in
            String result;
            HttpGetRequest getRequest = new HttpGetRequest();
            try {
                result = getRequest.execute(myUrl).get();
                System.out.println(result);
                eanTxtView.setText(result);
            } catch (ExecutionException e) {
                e.printStackTrace();
            } catch (InterruptedException e) {
                e.printStackTrace();
            }

        }

        anotherScan.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                    startActivity(new Intent(EanActivity.this, MainActivity.class));
            }
        });

    }

}
