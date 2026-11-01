import { provideHttpClient } from '@angular/common/http';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { environment } from '../../../../../environments/environment';
import { RestaurantList } from './restaurant-list';

describe('Restaurant pagination', () => {
  let component: RestaurantList;
  let http: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [provideHttpClient(), provideHttpClientTesting()],
    });
    component = TestBed.createComponent(RestaurantList).componentInstance;
    http = TestBed.inject(HttpTestingController);
  });

  afterEach(() => http.verify());

  function expectPage(index: number, size: number) {
    return http.expectOne(request =>
      request.url === `${environment.apiUrl}/restaurants` &&
      request.params.get('pageIndex') === String(index) &&
      request.params.get('pageSize') === String(size),
    );
  }

  it('converts Material indexes to API indexes and stores the total count', () => {
    component.onPageChange({ pageIndex: 1, pageSize: 6, length: 20 });
    expect(component.loading()).toBe(true);
    expectPage(2, 6).flush({ items: [], totalCount: 20, totalPages: 4, itemsFrom: 7, itemsTo: 12 });
    expect(component.pageIndex()).toBe(2);
    expect(component.totalCount()).toBe(20);
    expect(component.loading()).toBe(false);
  });

  it('returns to the first page when page size changes', () => {
    component.pageIndex.set(3);
    component.onPageChange({ pageIndex: 4, pageSize: 3, length: 20 });
    expectPage(1, 3).flush({ items: [], totalCount: 20, totalPages: 7, itemsFrom: 1, itemsTo: 3 });
    expect(component.pageIndex()).toBe(1);
    expect(component.pageSize()).toBe(3);
  });

  it('shows an English error and retries the selected page', () => {
    component.onPageChange({ pageIndex: 1, pageSize: 6, length: 20 });
    expectPage(2, 6).flush(null, { status: 500, statusText: 'Server error' });
    expect(component.error()).toBe('Unable to load restaurants. Please try again.');
    expect(component.loading()).toBe(false);
    component.loadRestaurants();
    expect(component.error()).toBeNull();
    expectPage(2, 6).flush({ items: [], totalCount: 20, totalPages: 4, itemsFrom: 7, itemsTo: 12 });
    expect(component.loading()).toBe(false);
  });
});
