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

export interface ProductItem {
    id: number;
    name: string;
    price: number;
    brand: string;
    category: string;
    colors: string[];
}

export interface Products {
    message: string;
    products: ProductItem[];
}