import SetModel from "./SetModel";

export default interface ExerciseModel{
    id : string,
    idWorkout:string
    exerciseName: string,
    sets: SetModel
}
