
import { NgModule } from '@angular/core';
import { CDataStatistics } from './storico-lotto/modali/modelData/LottoModelData';
import { inputData } from './confronto-siti/modali/modelData/InputData';



@NgModule({
    providers: [
        CDataStatistics,
        inputData
    ]
})
export class InternalClassModule {  }
