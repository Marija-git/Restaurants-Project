import { Component, input } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { Restaurant } from '../../data-access/restaurant.model';

@Component({
  selector: 'app-restaurant-card',
  imports: [MatCardModule],
  templateUrl: './restaurant-card.html',
  styleUrl: './restaurant-card.scss',
})
export class RestaurantCard {
  // The parent supplies data; this component only renders it.
  readonly restaurant = input.required<Restaurant>();
}
