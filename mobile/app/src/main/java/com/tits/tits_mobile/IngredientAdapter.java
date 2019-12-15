package com.tits.tits_mobile;

import android.app.Activity;
import android.content.Context;
import android.graphics.Movie;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.BaseAdapter;
import android.widget.ImageView;
import android.widget.TextView;

import java.util.ArrayList;


public class IngredientAdapter extends BaseAdapter {

    public class ViewHolder {
        public ImageView img;
        public TextView txt;
    }

    private Activity context_1;
    private ArrayList<IngredientEntry> pairs;

    public IngredientAdapter (Activity context, ArrayList<IngredientEntry> pairs) {
        context_1 = context;
        this.pairs = pairs;
    }

    @Override
    public int getCount() {
        return pairs.size();
    }

    @Override
    public Object getItem(int position) {
        return null;
    }

    @Override
    public long getItemId(int position) {
        return 0;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        ViewHolder viewHolder = null;

        if (convertView == null) {
            convertView = LayoutInflater.from(context_1).inflate(R.layout.list_item, null);
            viewHolder = new ViewHolder();

            viewHolder.img = (ImageView) convertView
                    .findViewById(R.id.hazardImg);
            viewHolder.txt = (TextView) convertView
                    .findViewById(R.id.ingName);

            convertView.setTag(viewHolder);
        } else {
            viewHolder = (ViewHolder) convertView.getTag();
        }

        viewHolder.txt.setText(pairs.get(position).getmIngredientName());
        int id = context_1.getResources().getIdentifier(pairs.get(position).getmIngredientHazardImage(), "drawable", context_1.getPackageName());
        viewHolder.img.setImageResource(id);

        return convertView;
    }


}
