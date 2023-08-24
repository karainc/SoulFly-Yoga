import {useEffect, useState} from "react"
import {useNavigate, useParams} from "react-router-dom"
import {updateRoutine, getRoutineById} from "../Managers/RoutineManager"
import {getAllPoses} from "../Managers/PoseManager"
import { Button, Form, FormGroup, Input, Label } from "reactstrap"

//SEE RoutineFORM FOR SIMILAR CODE AND EXPLANATION
//Notice that useParams is crucial for our edit. This is how we get the data for the specific Routine we are on as well as PUT to the database, overriding the old information with our newly updated Inputs
export const RoutineEdit = () => {
    const navigate = useNavigate()
    const localSoulFlyUser = localStorage.getItem("user");
    const soulFlyUserObject = JSON.parse(localSoulFlyUser)
    const { routineId } = useParams();
    const [poses, setPoses] = useState([])
    
    const getPoses = () => {
        getAllPoses().then(allPoses => setPoses(allPoses));
    }
    
    useEffect(() => {
        getPoses()
    }, [])
 
    const [routine, update] = useState({
        intention: "",
        reflection: null,
        cycles: 0,
        userId: soulFlyUserObject,
        creationDate: Date(),
        poseId: 0
    })

    useEffect(() => {
        getRoutineById(routineId)
        .then((routineArray) => {
            update(routineArray)
        })
    }, [routineId]);

    const handleSaveButtonClick = (event) => {
        event.preventDefault()

        const routineToEdit = {
            Id: parseInt(routineId),
            Intention: routine.intention,
            Reflection: routine.reflection,
            Cycles: routine.cycles,
            PoseId: routine.poseId,
            UserId: routine.userId
        }

        if (routineToEdit.PoseId < 0) {
        return window.alert("There is no routine without a pose!")}
        else{
            return updateRoutine(routineToEdit)
            .then(() => {
                navigate(`/routines`)
            })
        } 
    }

    const selectList = (event) => {
        const copy = {
            ...routine
        }
        copy.routineId = event.target.value
        update(copy)
    }

    return (
        <div className="routine-form-container">
            <Form className="routine-edit-form">
                <h2 className="routineForm">New Routine</h2>

                <FormGroup>
                    <div className="form-group-pose-select">
                        <Label htmlFor="pose-select">Your Pose</Label><p></p>
                        <select id="type"
                            required
                            value={
                                routine.poseId
                            }
                            onChange={
                                event => selectList(event)
                        }>
                            <option value="">Select your poses</option>
                            {
                            poses.map(pose => {
                                return <option value={pose.id} key={
                                    pose.id
                                }>
                                    {
                                    pose.name
                                }</option>
                                
                        })
                        } </select>  
                        </div>
                </FormGroup>
                <FormGroup>
                    <div className="form-group">
                        <Label htmlFor="intention">Set your Intention</Label>
                        <Input id="intention" type="text" className="form-control"
                            value={
                                routine.intention
                            }
                            onChange={
                                (event) => {
                                    const copy = {
                                        ...routine
                                    }
                                    copy.intention = event.target.value
                                    update(copy)
                                }
                            }/>
                    </div>
            </FormGroup>
            <FormGroup>
                <div className="form-group">
                    <Label htmlFor="cycle">Cycles</Label>
                    <Input id="cycle" type="text" className="form-control"
                        value={
                            routine.cycles
                        }
                        onChange={
                            (event) => {
                                const copy = {
                                    ...routine
                                }
                                copy.cycles = event.target.value
                                update(copy)
                            }
                        }/>
                </div>
        </FormGroup>


    <Button className="btn btn-primary"
        onClick={
            (clickEvent) => handleSaveButtonClick(clickEvent)
    }>
        Save Your Routine</Button>
</Form></div>
    )


}