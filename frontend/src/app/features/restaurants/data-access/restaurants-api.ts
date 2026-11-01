import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { PaginatedResult } from '../../../shared/models/paginated-result';
import { Restaurant } from './restaurant.model';

@Injectable({ providedIn: 'root' })
export class RestaurantsApi {
  private readonly http = inject(HttpClient);
  private readonly url = `${environment.apiUrl}/restaurants`;

  getAll(pageIndex: number, pageSize: number) {
    return this.http.get<PaginatedResult<Restaurant>>(this.url, {
      params: { pageIndex, pageSize },
    });
  }
}
