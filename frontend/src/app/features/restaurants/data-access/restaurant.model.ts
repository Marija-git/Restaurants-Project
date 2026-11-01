export interface Dish {
  id: number;
  name: string;
  description: string;
  price: number;
  kiloCalories: number | null;
}

export interface Restaurant {
  id: number;
  name: string;
  description: string;
  category: string;
  hasDelivery: boolean;
  city: string | null;
  street: string | null;
  postalCode: string | null;
  logoSasUrl: string | null;
  dishes: Dish[];
}
