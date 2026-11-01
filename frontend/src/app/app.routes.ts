import { Routes } from '@angular/router';

export const routes: Routes = [
  { path: '', redirectTo: 'restaurants', pathMatch: 'full' },
  {
    path: 'restaurants',
    // Load the page component only when this route is opened.
    loadComponent: () =>
      import('./features/restaurants/pages/restaurant-list/restaurant-list').then(
        (module) => module.RestaurantList,
      ),
  },
];
