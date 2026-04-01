import React, {useState} from "react";

function DisplayCard({ title, value, onChange, style, step }) {
    const [internalCount, setInternalCount] = useState(0);

    return (
        <div 
            style={{
                border: '1px solid #ccc',
                borderRadius: '8px',
                padding: '20px',
                margin: '10px',
                width: '200px',
                textAlign: 'center',
                ...style
            }}
        >
            <h3>{title}</h3>
            <p>Props Value from parent: {value}</p>

            <p>Internal Count: {internalCount}</p>

            <button onClick={() => setInternalCount(internalCount + 1)}>
                Update Internal Count
            </button>

            <button onClick={() => onChange(value + step)}>
                Update Parent Count
            </button>
        </div>
    );
}

function StateVsPropsDemo() {
    const [parentCount, setParentCount] = useState(0);
    const [parentStep, setParentStep] = useState(1);
    const [displayColor, setDislayColor] = useState('lightblue');

    const handleParentCountChange = (newCount) => {
        setParentCount(newCount);
        setDislayColor(newCount % 2 === 0 ? 'lightblue' : 'lightcoral');
    };

    return (
        <div style={{display: 'flex', justifyContent: 'center', padding: '20px'}}>
            <p>Parent Count: {parentCount}</p>
            <button onClick={() => setParentStep(parentStep + 1)} 
                style={{marginLeft: '20px'}}>
                Increase Step (Current: {parentStep})
            </button>
            <button onClick={() => setDislayColor
                (displayColor === 'lightblue' ? 'lightcoral' : 'lightblue')} 
                style={{marginLeft: '20px'}}>
                Toggle Display Color
            </button>
            <DisplayCard
                title="Child Component 1 Counter Card" 
                value={parentCount} 
                onChange={handleParentCountChange} 
                step={parentStep}
                style={{backgroundColor: displayColor}}
            />
            <DisplayCard
                title="Child Component 2 Counter Card" 
                value={parentCount} 
                onChange={handleParentCountChange} 
                step={parentStep} 
                style={{backgroundColor: displayColor}}
            />
        </div>
    );
}

export default StateVsPropsDemo;