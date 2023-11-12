import {observer} from "mobx-react-lite";
import React, {useEffect, useState} from 'react';
import {Button, Card, DatePicker, Input, message, Popconfirm} from "antd";
import exercise from "../../state/exercise";
import {DeleteFilled, EditFilled, PlusCircleFilled} from "@ant-design/icons";
import Set from "../set/Set";
import set from "../../state/set";

interface exercisePropsType {
    id: string,
    name: string
}

const Exercise: React.FC<exercisePropsType> = observer(({id, name}) => {
    const [exerciseName, setExerciseName] = useState(name);

    useEffect(()=>{
        setExerciseName(name);
    }, [name])

    const confirm = (e: React.MouseEvent<HTMLElement> | undefined) => {
        exercise.deleteExercise(id).then(t => {
            message.success('Успешно удалено');
        }).catch(e => {
            message.error('Ошибка')
        })
    };

    console.log(exercise.exercises)

    return (
        <Card
            style={{width: '100%'}}
            title={<div className='workoutCard-headerTitle'>
                <Input
                    value={exerciseName}
                    onChange = {(e)=>setExerciseName(e.target.value)}
                    onBlur={(e) => exercise.updateExercise(id, e.target.value)}
                ></Input>
            </div>}
            extra={
                <div>
                    <Button
                        size={"large"}
                        icon={<PlusCircleFilled/>}
                        title='Добавить упражнение'
                        onClick={(e) => set.createNewSet(id)}
                        className='bg-success text-black-50'>

                    </Button>
                    <Popconfirm
                        title="Удалить тренировку"
                        description="Уверены?"
                        onConfirm={confirm}
                        okText="Да"
                        cancelText="Нет"
                    >
                        <Button size={"large"} icon={<DeleteFilled/>} className='bg-danger text-black-50'></Button>
                    </Popconfirm>
                </div>
            }
        >
            <div>
                {exercise.exercises.filter(e => e.id === id).map(ex =>{
                    return ex.sets.map(s => <Set id={s.id} repetitionNuber={s.repetitionNumber} setWeight={s.setWeight}/>)
                })}
            </div>
        </Card>
    );
});

export default Exercise;
