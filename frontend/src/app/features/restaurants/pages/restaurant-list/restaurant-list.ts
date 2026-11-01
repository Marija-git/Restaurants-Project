import { Component, DestroyRef, inject, OnInit, signal } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { finalize } from 'rxjs';
import { MatButtonModule } from '@angular/material/button';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { RestaurantCard } from '../../ui/restaurant-card/restaurant-card';
import { RestaurantsApi } from '../../data-access/restaurants-api';
import { Restaurant } from '../../data-access/restaurant.model';

@Component({
  selector: 'app-restaurant-list',
  imports: [MatButtonModule, MatPaginatorModule, MatProgressSpinnerModule, RestaurantCard],
  templateUrl: './restaurant-list.html',
  styleUrl: './restaurant-list.scss',
})
export class RestaurantList implements OnInit {
  private readonly restaurantsApi = inject(RestaurantsApi);
  private readonly destroyRef = inject(DestroyRef);

  readonly restaurants = signal<Restaurant[]>([]);
  readonly loading = signal(false);
  readonly error = signal<string | null>(null);

  readonly pageIndex = signal(1);
  readonly pageSize = signal(6);
  readonly totalCount = signal(0);
  // These sizes match the backend's pagination validation.
  readonly pageSizeOptions = [3, 6, 10];

  onPageChange(event: PageEvent): void {
    const sizeChanged = event.pageSize !== this.pageSize();
    this.pageSize.set(event.pageSize);
    // Material indexes start at zero, while API indexes start at one.
    // Changing page size resets the list to the first page.
    this.pageIndex.set(sizeChanged ? 1 : event.pageIndex + 1);
    this.loadRestaurants();
  }

  ngOnInit(): void {
    // Angular calls this once when the component is initialized.
    this.loadRestaurants();
  }

  loadRestaurants(): void {
    // Start a new request and clear any previous error.
    this.loading.set(true);
    this.error.set(null);

    this.restaurantsApi
      .getAll(this.pageIndex(), this.pageSize())
      .pipe(
        // Cancel the subscription if the component is destroyed.
        takeUntilDestroyed(this.destroyRef),
        // Stop loading on success, error, or cancellation.
        finalize(() => this.loading.set(false)),
      )
      // Subscribing sends the HTTP request.
      .subscribe({
        next: (response) => {
          // Store the current page of restaurants and the total result count.
          this.restaurants.set(response.items);
          this.totalCount.set(response.totalCount);
        },
        error: () => {
          this.error.set('Unable to load restaurants. Please try again.');
        },
      });
  }
}
