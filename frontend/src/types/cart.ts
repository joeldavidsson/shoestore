export interface Cart {
  id: number;
  userId: string;
  items: CartItem[];
  createdAt: Date;
  lastModified?: Date;
  totalAmount: number;
}

export interface CartItem {
  id?: number;
  productId: number;
  name: string;
  description?: string;
  brand?: string;
  price: string;
  imageUrl?: string;
  quantity: number;
}
