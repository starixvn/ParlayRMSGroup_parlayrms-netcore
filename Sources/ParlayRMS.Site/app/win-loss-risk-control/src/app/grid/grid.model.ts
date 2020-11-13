export enum GridType {
    Site,
    Company,
    Member
}

export interface Grid {
    hasExcel: boolean;
    title: string;
    query(): void;
    exportExcel(): void;
}
