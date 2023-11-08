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
export interface Brand{
    id?: string;
    name?: string;
}
export interface Category{
    id?: string;
    name?: string;
}
export interface Color{
    name?: string;
}
export interface Product {
    id: string;
    name: string;
    price: number;
    brand: Brand;
    category: Category;
    colors: Color[];
    stock: number;
    description: string;
}
export interface GetProductReponse{
    id: string;
    name: string;
    price: number;
    brand: Brand;
    category: Category;
    stock: number;
    colors: Color[];
    description: string;
}

export interface GetProductsResponse {
    message: string;
    products: Product[],
    brands: Brand[],
    categories: Category[]
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

export interface CartItem {
    id: string,
    name: string;
    price: number;
    cant: number;
}

export interface PurchaseResponse {
    message: string,
    Purchase: {
        ProductNames: string[],
        UserEmail: string,
        TotalPrice: number,
        FinalPrice: number,
        PromotionName?: string
    }
}
export interface CreateProductRequest{
    name?: string;
    price?: number;
    brand?: Brand;
    category?: Category;
    colors?: string[];
    stock?: number;
    description?: string;
}
export interface UpdateProductRequest{
    id?: string;
    name?: string;
    price?: number;
    brand?: Brand;
    category?: Category;
    colors?: string[];
    stock?: number;
    description?: string;
}