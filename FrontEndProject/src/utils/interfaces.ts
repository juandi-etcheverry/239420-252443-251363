export interface GetUserResponse {
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

export interface SignupResponse {
    message: string;
}