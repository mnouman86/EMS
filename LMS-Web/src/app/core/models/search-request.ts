/** Mirrors the backend CleanArc.Application.Models.Request.SearchRequest. */
export interface FilterParameter {
  parameterName: string;
  parameterValue: string;
}

export interface SortingParameter {
  sortingColumnName: string;
  sortingColumnDirection: 'ASC' | 'DESC';
}

export interface SearchRequest {
  pageNumber: number;
  pageSize: number;
  cultureId?: number | null;
  userId?: number | null;
  filterArray: FilterParameter[];
  sortingArray: SortingParameter[];
}

/** Mirrors DeleteRequest (incl. the ForceHard flag added for hard-delete guards). */
export interface DeleteRequest {
  selectedIds: string;
  cultureId?: number | null;
  isDeleted: boolean;
  forceHard?: boolean;
}

export function defaultSearch(partial: Partial<SearchRequest> = {}): SearchRequest {
  return {
    pageNumber: 1,
    pageSize: 50,
    cultureId: null,
    userId: null,
    filterArray: [],
    sortingArray: [],
    ...partial
  };
}
