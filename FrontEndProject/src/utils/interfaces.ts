export interface GetUserResponse {
    id: string;
    message: string;
    email: string;
    address: string;
    role: number;
};

export interface UpdateUserResponse extends GetUserResponse {};

export interface ErrorStatus {
    status: number;
    error: {
        message: string;
    }
}

export interface Product {
    id: string;
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

export interface SignupResponse {
    message: string;
}

export interface SignupRequest {
    email: string;
    address: string;
    password: string;
    passwordConfirmation: string;
}
export interface LoginRequest {
    email: string;
    password: string;
}
export interface LoginResponse {
    message: string;
    email: string;
    Address: string;
    Role: number;
}
export interface PurchaseRequest{
    cart : {id : string, cant : number}[];
}