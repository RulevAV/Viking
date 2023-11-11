import Base from "./base";
import WorkoutModel from "../models/WorkoutModel";

class WorkoutService {
    controller = "Workout/"
    constructor() {
    }

    async CreateNewWorkout(workoutName:string) {
        return await Base.post<WorkoutModel>(this.controller + "CreateNewWorkout",{workoutName}) as WorkoutModel;
    }
    async GetAllWorkout() {
        return await Base.get<WorkoutModel[]>(this.controller + "GetAllWorkout") as WorkoutModel[];
    }

    async UpdateWorkout(id:string,workoutName:string) {
        return await Base.put<WorkoutModel>(this.controller + "UpdateWorkout",{id,workoutName}) as WorkoutModel;
    }
    async DeleteWorkout(id:string) {
        return await Base.delete<WorkoutModel>(this.controller + `DeleteWorkout/${id}`) as WorkoutModel;
    }
}

const workoutService = new WorkoutService();
export default workoutService;
