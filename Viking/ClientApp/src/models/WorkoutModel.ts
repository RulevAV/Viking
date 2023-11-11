import ExerciseModel from "./ExerciseModel";

export default interface WorkoutModel{
    id : string,
    idUser?:string,
    workoutName: string,
    DateOfWeek?: Date
    exercises:ExerciseModel[]
}