import { BrowserRouter as Router, Route, Routes } from "react-router-dom"
import {ApplicationViews} from "./ApplicationViews"

const App=() => {
  return <Router>
    <Routes>
    <Route path="*" element={
            <>
              <div className="content-container">
                    <div className="content-container">
                <ApplicationViews />  
              </div>
                </div>
          </>
        } />
      </Routes>
  </Router>
}

export default App;
