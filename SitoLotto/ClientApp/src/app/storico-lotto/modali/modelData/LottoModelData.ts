export interface DataStatistics {
    Data: any;
    Min: number;
    Max: number;
    Average: number;
}
export class CDataStatistics implements DataStatistics {
    Data: any = '';
    Min: number = 0;
    Max: number = 0;
    Average: number = 0.0;
    constructor() { }
}
