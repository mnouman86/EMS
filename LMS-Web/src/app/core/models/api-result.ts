/**
 * Mirrors the response envelope produced by every Web API controller's
 * `Wrap()` / OperationResult handler:
 *   { Data, Message, StatusCode, IsSuccess, TotalCount }
 */
export interface ApiResult<T> {
  data: T;
  message: string;
  statusCode: number;
  isSuccess: boolean;
  totalCount?: number;
}

/** Raw shape as it arrives over the wire (PascalCase from the .NET API). */
export interface RawApiResult<T> {
  Data: T;
  Message: string;
  StatusCode: number;
  IsSuccess: boolean;
  TotalCount?: number;
}
