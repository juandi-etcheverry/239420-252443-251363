export interface GetUserResponse {
    message: string;
    email: string;
    address: string;
    role: number;
};

export interface UpdateUserResponse extends GetUserResponse {};

export interface ErrorStatus {
    status: number;
}

export interface Product {
    id: number;
    name: string;
    price: number;
    brand: string;
    category: string;
    colors: string[];
}

export interface GetProductsResponse {
    message: string;
    products: Product[],
    brands: string[],
    categories: string[]
};

export interface ProductFilterForm {
    textInput?: string | null;
    brandInput?: string | null;
    categoryInput?: string | null;
    minPrice?: number | null;
    maxPrice?: number | null;
}

export interface UpdateUserProps {
    id?: string | null;
    email?: string  | null;
    address?: string | null;
    role?: number | null;
}