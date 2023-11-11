import Base from "./base";
import ExerciseModel from "../models/ExerciseModel";

class ExerciseService {
    controller = "Exercise/"
    constructor() {
    }

    async CreateNewExercise(idWorkout:string,exerciseName:string) {
        return await Base.post<ExerciseModel>(this.controller + "CreateNewExercise",{idWorkout,exerciseName}) as ExerciseModel;
    }
    async GetAllWorkout() {
        return await Base.get<ExerciseModel[]>(this.controller + "GetAllWorkout") as ExerciseModel[];
    }

    async UpdateExercise(id:string,workoutName:string) {
        return await Base.put<ExerciseModel>(this.controller + "UpdateExercise",{id,workoutName}) as ExerciseModel;
    }
    async DeleteExercise(id:string) {
        return await Base.delete<ExerciseModel>(this.controller + `DeleteExercise/${id}`) as ExerciseModel;
    }
}

const exerciseService = new ExerciseService();
export default exerciseService;
