import { useState } from "react"
import { useParams } from "react-router-dom"
import { useNavigate } from "react-router-dom"
import { Button, FormGroup, Input, Label } from "reactstrap"
import { addComment } from "../Managers/CommentManager.js"
import './Comment.css';

export const CommentForm = () => {

    //GUESS WHAT! THIS HAS TO MATCH THE ID IN THE URL OF THE ROUTE IN APPVIEWS!
    const { routineId } = useParams();

    const localSoulFlyUser = localStorage.getItem("users")
    const soulFlyUserObject = JSON.parse(localSoulFlyUser)
    const navigate = useNavigate();
    const currentDate = new Date();
    const offset = currentDate.getTimezoneOffset();
    const timezoneOffset = offset * 60 * 1000;
    const correctedDate = new Date(currentDate.getTime() - timezoneOffset)

    const [newComment, updateComment] = useState({
        RoutineId: routineId,
        UsersId: soulFlyUserObject.id,
        text: "",

        CreateDateTime: correctedDate.toISOString()
    })

    const handleSaveButtonClick = (e) => {
        e.preventDefault()

        const commentToSendToAPI = {
            RoutineId: routineId,
            UsersId: soulFlyUserObject.id,
            Text: newComment.text,
            CreateDateTime: correctedDate.toISOString()
        }

        console.log(routineId)
        addComment(commentToSendToAPI)
        .then(() => {
            if (routineId) {
                navigate(`/comments/${routineId}`);
            }
        });
    };

    

    return (
        <form className="comment-form">
        <h2 className="comment-form-title">Create a New Comment</h2>

            <FormGroup className="form-group">
                <Label htmlFor="text">text:</Label>
                <Input
                    className="comment-input"
                    type="text"
                    id="text"
                    value={newComment.text}
                    onChange={
                        (event) => {
                            const copy = { ...newComment }
                            copy.text = event.target.value
                            updateComment(copy)
                        }
                    } />
            </FormGroup>

        <Button
            onClick={(clickEvent) => handleSaveButtonClick(clickEvent)} className="btn btn-primary">Save Comment</Button>
    </form>
)
}   