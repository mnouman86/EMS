export interface LoginRequest {
  userName: string;
  password: string;
}

/** Token payload returned by the auth endpoint. Field names are normalised
 *  in AuthService to tolerate either camel/Pascal casing from the API. */
export interface AuthTokens {
  accessToken: string;
  refreshToken: string;
  expiresAtUtc?: string;
}

export interface CurrentUser {
  id: number;
  userName: string;
  email?: string;
  roles: string[];
  /** Backend flag: true when the user was just created (or admin-reset) and
   *  must pick their own password before doing anything else. */
  mustChangePassword?: boolean;
}
