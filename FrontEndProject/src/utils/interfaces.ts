export interface GetUserResponse {
    id: string;
    message: string;
    email: string;
    address: string;
    role: number;
};

export interface UpdateUserResponse extends GetUserResponse {};

export interface User{
    id: string;
    email: string;
    address: string;
    role: number;
}

export interface GetUsersResponse {
    users: User[];
}

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
    id: number;
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
    promotionsApply?:boolean
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
    promotionsApply?:boolean
}

export interface GetProductsResponse {
    message: string;
    products: Product[],
    brands: Brand[],
    categories: Category[],
    colors: Color[]
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

export interface CreateUserRequest extends SignupRequest {
    role: number;
}

export interface CreateUserResponse {
    user?: User;
    message: string;
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
    paymentMethod:string;
}

export interface CartItem {
    id: string,
    name: string;
    price: number;
    cant: number;
}

export interface PurchaseResponse {
    message: string,
    Purchase: SinglePurchase
}
export interface CreateProductRequest{
    name?: string;
    price?: number;
    brand?: Brand;
    category?: Category;
    colors?: Color[];
    stock?: number;
    description?: string;
    promotionsApply?:boolean
}
export interface UpdateProductRequest{
    id?: string;
    name?: string;
    price?: number;
    brand?: Brand;
    category?: Category;
    colors?: Color[];
    stock?: number;
    description?: string;
    promotionsApply?:boolean
}

export interface GetPromotionResponse{
    promotionName: string;
    discount: number;
    finalPrice: number;
}
export interface PurchaseProducts{
    ProductId: string;
    Quantity: number;
}

export interface SinglePurchase {
    productsNames: string[],
    userEmail: string,
    totalPrice: number,
    finalPrice: number,
    promotionName?: string
    paymentMethod?:string
}

export interface GetAllPurchasesResponse {
    purchases : SinglePurchase[];
}