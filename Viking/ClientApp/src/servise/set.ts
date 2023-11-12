import Base from "./base";
import SetModel from "../models/SetModel";
import WorkoutModel from "../models/WorkoutModel";

class SetService {
    controller = "Set/"
    constructor() {
    }

    async GetAllSet() {
        return await Base.get<SetModel[]>(this.controller + "GetAllSet") as SetModel[];
    }

    async CreateNewSet(idExercise:string) {
        return await Base.post<SetModel>(this.controller + "CreateNewSet",{idExercise}) as SetModel;
    }
    async UpdateSet(id:string,repetitionNumber:number,setWeight:number) {
        return await Base.put<SetModel>(this.controller + "UpdateSet",{id,repetitionNumber,setWeight}) as SetModel;
    }
    async DeleteSet(id:string) {
        return await Base.delete<SetModel>(this.controller + `DeleteSet/${id}`) as SetModel;
    }
}

const setService = new SetService();
export default setService;
