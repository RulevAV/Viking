import {observer} from "mobx-react-lite";
import './workoutCard.css'
import React, {createRef, useEffect, useState} from 'react';
import {Button, Card, DatePicker, Input, InputRef, message, notification, Popconfirm} from 'antd';
import {EditFilled, DeleteFilled, PlusCircleFilled} from "@ant-design/icons";
import dayjs from "dayjs";
import workout from "../../state/workout";
import exercise from "../../state/exercise";
import Exercise from "../exercise/Exersice";


interface workoutPropsType {
    title: string,
    id: string,
    tabs: { key: string, tab: string }[]
}

const Workout: React.FC<workoutPropsType> = observer(({title, id, tabs}) => {

    const contentList: Record<string, React.ReactNode> = {};
    const [activeTabKey1, setActiveTabKey1] = useState<string>(tabs[0]?.key);

    let data = new Date();
    let dateFormat = 'DD.MM.YYYY';

    tabs.forEach((t, index) => {
        contentList[t.key] =  <Exercise id={t.key} name={t.tab}/>
    })
    const onTab1Change = (key: string) => {
        setActiveTabKey1(key);
    };
    const confirm = (e: React.MouseEvent<HTMLElement> | undefined) => {
        workout.deleteWorkout(id).then(t => {
            message.success('Успешно удалено');
        }).catch(e => {
            message.error('Ошибка')
        })
    };


    return (
        <Card
            style={{width: '100%'}}
            title={<div className='workoutCard-headerTitle'>
                {/*<span>{title}</span>*/}
                <Input
                    defaultValue={title}
                    onBlur={(e) => workout.updateWorkout(id, e.target.value)}
                ></Input>
                <span>
                        <DatePicker
                            disabled={true}
                            defaultValue={dayjs(data.toLocaleDateString('ru-RU'), dateFormat)}
                            format={dateFormat}/>
                    </span>
            </div>}
            tabList={tabs}
            extra={
                <div>
                    <Button
                        size={"large"}
                        icon={<PlusCircleFilled/>}
                        title='Добавить упражнение'
                        onClick={(e) => exercise.createNewExercise(id, 'Новое упражнение')}
                        className='bg-success text-black-50'>

                    </Button>
                    <Button size={"large"} icon={<EditFilled/>} className='bg-info text-black-50'></Button>
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
            activeTabKey={activeTabKey1}
            onTabChange={onTab1Change}
        >

            {contentList[activeTabKey1]}
        </Card>
    );
});

export default Workout;
